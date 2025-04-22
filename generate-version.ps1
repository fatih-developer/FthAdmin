#region code: fatih.unal date: 2025-04-22
# Build sırasında her katmanın altına son git commit hash'ini yazan script
$hash = git rev-parse --short HEAD
$folders = @(
    "FthAdmin.Api",
    "FthAdmin.Application",
    "FthAdmin.Core",
    "FthAdmin.Domain",
    "FthAdmin.Infrastructure",
    "FthAdmin.Persistence"
)
foreach ($folder in $folders) {
    Set-Content -Path (Join-Path $folder "Version.txt") -Value $hash
}
Write-Host "Versiyon: $hash"
#endregion
