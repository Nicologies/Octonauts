Copy-Item ./LICENSE.txt ./Octonauts.Cli/LICENSE.txt
$pkg = (Get-Item ./nuget_packages/*.nupkg).FullName
New-Item -Type Directory ./temp -ErrorAction SilentlyContinue
Move-Item $pkg ./temp/temp.zip
Expand-Archive ./temp/temp.zip -DestinationPath ./temp
$content = Get-Content ./temp/Octonauts.Cli.nuspec -Raw
$replaceWith = @"
</metadata>
<files>
        <file src=".\publish\*.*" target="tools"/>
        <file src="..\LICENSE.txt"/>
        <file src=".\VERIFICATION.txt"/>
    </files>
"@
$content = $content.Replace("</metadata>", $replaceWith)
Set-Content ./Octonauts.Cli/Octonauts.Cli.nuspec $content
