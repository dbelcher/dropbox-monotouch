using System;

namespace DropboxSDK
{
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

