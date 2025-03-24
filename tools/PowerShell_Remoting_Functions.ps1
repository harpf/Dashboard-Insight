function Invoke-ModuleUpdate {
    param (
        [string]$ComputerName,
        [string]$ModuleName,
        [string]$Version,
        [string]$Username,
        [string]$Password
    )

    $secPass = ConvertTo-SecureString $Password -AsPlainText -Force
    $cred = New-Object System.Management.Automation.PSCredential ($Username, $secPass)

    Invoke-Command -ComputerName $ComputerName -Credential $cred -ScriptBlock {
        param ($ModuleName, $Version)
        if (-not (Get-Module -ListAvailable -Name $ModuleName)) {
            Install-Module -Name $ModuleName -RequiredVersion $Version -Force -AllowClobber
        } else {
            Install-Module -Name $ModuleName -RequiredVersion $Version -Force -AllowClobber
        }
    } -ArgumentList $ModuleName, $Version
}