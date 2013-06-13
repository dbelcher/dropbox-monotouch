#!/bin/sh

BUILDDIR=build
PROJNAME=DropboxSDK

PROJDIR=dropbox-sdk-ios/$PROJNAME
MTPROJDIR=../$PROJNAME
PROJ=$PROJNAME.xcodeproj
TARGET=S$PROJNAME

echo "==== Building for iOS devices ===="
echo ""
xcodebuild -project $PROJDIR/$PROJ -target $TARGET

echo "==== Building for iOS simulator ===="
echo ""
xcodebuild -project $PROJDIR/$PROJ -target $TARGET -sdk iphonesimulator

mkdir -p $BUILDDIR

cp $PROJDIR/build/Release-iphonesimulator/lib$TARGET.a $BUILDDIR/lib$PROJNAME-i386.a
cp $PROJDIR/build/Release-iphoneos/lib$TARGET.a $BUILDDIR/lib$PROJNAME-armv7.a

echo "Creating fat binary"
lipo -create $BUILDDIR/lib$PROJNAME-i386.a $BUILDDIR/lib$PROJNAME-armv7.a -output $BUILDDIR/lib$PROJNAME.a

cp $BUILDDIR/lib$PROJNAME.a $MTPROJDIR/lib$PROJNAME.a

echo "Done! ShareKit iOS Lib built: $PROJNAME"
