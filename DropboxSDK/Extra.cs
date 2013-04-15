using System;
using MonoTouch.Foundation;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace DropboxSDK
{
    public partial class DBRestClient : NSObject
    {
        public Task<DBMetadata> LoadMetadataTask (string path, string hash = null, bool needDirContents = false, CancellationToken token = default (CancellationToken)) 
        {
            var tcs = new TaskCompletionSource<DBMetadata> ();
            if (token.IsCancellationRequested) {
                tcs.TrySetCanceled ();
                return tcs.Task;
            }
            
            BeginInvokeOnMainThread (() => {
                if (token.IsCancellationRequested) {
                    tcs.TrySetCanceled ();
                    return;
                }

                MetadataLoaded += (sender, e) => {
                    if (token.IsCancellationRequested) {
                        tcs.TrySetCanceled ();
                        return;
                    }
                    tcs.TrySetResult (e.Metadata);
                };

                LoadMetadataFailed += (sender, e) => {
                    if (token.IsCancellationRequested) {
                        tcs.TrySetCanceled ();
                        return;
                    }
                    tcs.TrySetException (new DropboxException (e.Error));
                };

                MetadataUnchangedAtPath += (sender, e) => {
                    if (token.IsCancellationRequested) {
                        tcs.TrySetCanceled ();
                        return;
                    }
                    tcs.TrySetException (new DropboxException (null, HttpStatusCode.NotModified));
                };
            
                var args = new NSMutableDictionary ();
                if (hash != null)
                    args [new NSString ("hash")] = new NSString (hash);
                if (needDirContents)
                    args [new NSString ("list")] = new NSString ("true");
            
                LoadMetadata (path, args);
            });
            
            return tcs.Task;
        }
        
        public Task<DBMetadata> LoadThumbnailTask (string path, string size, string destPath, CancellationToken token = default (CancellationToken)) 
        {
            var tcs = new TaskCompletionSource<DBMetadata> ();
            if (token.IsCancellationRequested) {
                tcs.TrySetCanceled ();
                return tcs.Task;
            }
            
            BeginInvokeOnMainThread (() => {
                if (token.IsCancellationRequested) {
                    tcs.TrySetCanceled ();
                    return;
                }

                ThumbnailMetadataLoaded += (sender, e) => {
                    if (token.IsCancellationRequested) {
                        tcs.TrySetCanceled ();
                        return;
                    }
                    tcs.TrySetResult (e.Metadata);
                };
                
                LoadThumbnailFailed += (sender, e) => {
                    if (token.IsCancellationRequested) {
                        tcs.TrySetCanceled ();
                        return;
                    }
                    tcs.TrySetException (new DropboxException (e.Error));
                };
            
                LoadThumbnail (path, size, destPath);
            });
            
            return tcs.Task;
        }
        
        private class DBLoadFileRestClientDelegate : DBRestClientDelegate
        {
            public Action<string, string, DBMetadata> OnFileMetadataLoaded { get; set; }
            public Action<float, string> OnLoadProgress { get; set; }
            public Action<NSError> OnLoadFileFailed { get; set; }
            
            public override void FileMetadataLoaded (DBRestClient client, string destPath, string contentType, DBMetadata metadata)
            {
                if (OnFileMetadataLoaded != null)
                    OnFileMetadataLoaded (destPath, contentType, metadata);
            }
            
            public override void LoadFileFailed (DBRestClient client, NSError error)
            {
                if (OnLoadFileFailed != null)
                    OnLoadFileFailed (error);
            }
            
            public override void LoadProgress (DBRestClient client, float progress, string destPath)
            {
                if (OnLoadProgress != null)
                    OnLoadProgress (progress, destPath);
            }
        }

        public Action<float, string> OnLoadProgress { get; set; }
        
        public Task<DBMetadata> LoadFileTask (string path, string destPath, CancellationToken token = default (CancellationToken)) 
        {
            var tcs = new TaskCompletionSource<DBMetadata> ();
            if (token.IsCancellationRequested) {
                tcs.TrySetCanceled ();
                return tcs.Task;
            }
            
            // workaround: loadedFile selector is always defined in autogenerated delegate, hence using custom delegate

            BeginInvokeOnMainThread (() => {
                if (token.IsCancellationRequested) {
                    tcs.TrySetCanceled ();
                    return;
                }

                Delegate = new DBLoadFileRestClientDelegate {
                    OnFileMetadataLoaded = (_destPath, _contentType, _metadata) => {
                        if (token.IsCancellationRequested) {
                            tcs.TrySetCanceled ();
                            return;
                        }
                        tcs.TrySetResult (_metadata);
                    },
                    OnLoadFileFailed = (_error) => {
                        if (token.IsCancellationRequested) {
                            tcs.TrySetCanceled ();
                            return;
                        }
                        tcs.TrySetException (new DropboxException (_error));
                    },
                    OnLoadProgress = (_progress, _destPath) => {
                        if (token.IsCancellationRequested) {
                            tcs.TrySetCanceled ();
                            return;
                        }
                        if (this.OnLoadProgress != null) this.OnLoadProgress (_progress, _destPath);
                    }
                };

                LoadFile (path, destPath);
            });

            return tcs.Task;
        }

    }

    public partial class DBMetadata
    {
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty (Path))
                {
                    return string.Empty;
                }
                
                if (Path.LastIndexOf ("/") == -1)
                {
                    return string.Empty;
                }
                
                return string.IsNullOrEmpty (Path) ? "root" : Path.Substring (Path.LastIndexOf ("/") + 1);
            }
        }
        
        public string Extension
        {
            get
            {
                if (string.IsNullOrEmpty (Path))
                {
                    return string.Empty;
                }
                
                if (Path.LastIndexOf (".") == -1)
                {
                    return string.Empty;
                }
                
                return IsDirectory ? string.Empty : Path.Substring (Path.LastIndexOf ("."));
            }
        }
    }
}

