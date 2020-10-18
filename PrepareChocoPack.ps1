Copy-Item ./LICENSE.txt ./Octonauts.Cli/LICENSE.txt
$pkg = (Get-Item ./nuget_packages/*.nupkg).FullName
Remove-Item -Recurse -Force ./temp -ErrorAction SilentlyContinue
New-Item -Type Directory ./temp -ErrorAction SilentlyContinue
Copy-Item $pkg ./temp/temp.zip
Expand-Archive ./temp/temp.zip -DestinationPath ./temp
$content = Get-Content ./temp/OctonautsCli.nuspec -Raw
$replaceWith = @"
</metadata>
  <files>
    <file src=".\publish\*.*" target="tools"/>
    <file src="..\LICENSE.txt"/>
    <file src=".\VERIFICATION.txt"/>
  </files>
"@
$content = $content.Replace("</metadata>", $replaceWith)
Set-Content ./Octonauts.Cli/OctonautsCli.nuspec $content
