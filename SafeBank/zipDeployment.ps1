Function ZipEverything($src, $dest)
{
   Add-Type -AssemblyName System.IO.Compression.FileSystem
   [System.IO.Compression.ZipFile]::CreateFromDirectory($src, $dest)
}

ZipEverything -src "E:\Development\Websites\MVC\SafeBank\SafeBank\Deployment\src" -dest "E:\Development\Websites\MVC\SafeBank\SafeBank\Deployment\safebank.zip"