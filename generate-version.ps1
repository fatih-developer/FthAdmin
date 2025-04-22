#region code: fatih.unal date: 2025-04-22
# Build sırasında GitVersion ile versiyon numarasını alıp her katmanın altına bir dosyaya yazan script
dotnet tool install --global GitVersion.Tool;
$version = dotnet gitversion /showvariable FullSemVer;
$folders = @(
    "FthAdmin.Api",
    "FthAdmin.Application",
    "FthAdmin.Core",
    "FthAdmin.Domain",
    "FthAdmin.Infrastructure",
    "FthAdmin.Persistence"
)
foreach ($folder in $folders) {
    Set-Content -Path (Join-Path $folder "Version.txt") -Value $version
}
Write-Host "Versiyon: $version"
#endregion
