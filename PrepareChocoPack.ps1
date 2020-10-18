Copy-Item ./LICENSE.txt ./Octonauts.Cli/LICENSE.txt
$pkg = (Get-Item ./nuget_packages/*.nupkg).FullName
Remove-Item -Recurse -Force ./temp -ErrorAction SilentlyContinue
New-Item -Type Directory ./temp -ErrorAction SilentlyContinue
Copy-Item $pkg ./temp/temp.zip
Expand-Archive ./temp/temp.zip -DestinationPath ./temp
$content = Get-Content ./temp/OctonautsCli.nuspec -Raw
$meta = ([xml]$content).package.metadata
$projUrl = $meta.projectUrl
$replaceWith = @"
  <title>$($meta.id)</title>
  <owners>$($meta.authors)</owners>
  <releaseNotes>$projUrl/releases</releaseNotes>
  <packageSourceUrl>$projUrl</packageSourceUrl>
  <bugTrackerUrl>$projUrl/issues</bugTrackerUrl>
  <summary>$($meta.description)</summary>
</metadata>
  <files>
    <file src=".\publish\*.*" target="tools"/>
    <file src="..\LICENSE.txt"/>
    <file src=".\VERIFICATION.txt"/>
    <file src=".\logo.png"/>
  </files>
"@
$content = $content.Replace("</metadata>", $replaceWith)
Set-Content ./Octonauts.Cli/OctonautsCli.nuspec $content
