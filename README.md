# Npam
Npam is a wrapper around Linux-PAM for dotnet core.

##Compatibility
Npam has been tested on Ubuntu 16.04 (dotnet version 1.0.0-preview2-003121) and Fedora 23 (dotnet version 1.0.0-preview2-003121)

##Using Npam
The best way to make use of the Npam library is to include it as a nuget dependency. Npam is available on nuget at https://www.nuget.org/packages/Npam

Below is a sample project.json which includes a nuget dependency on Npam
```
{
  "name" : "Npam-sample",
  "description" : "A sample application which demonstrates usage of the Npam library.",
  "version": "1.0.0",
  "authors" : [ "Cameron Waldron" ],
  "buildOptions": {
    "debugType": "portable",
    "emitEntryPoint": true
  },
  "dependencies": { "Npam" : "1.0.0" },
  "frameworks": {
    "netcoreapp1.0": {
      "dependencies": {
        "Microsoft.NETCore.App": {
          "type": "platform",
          "version": "1.0.0"
        }
      },
      "imports": "dnxcore50"
    }
  }
}
```
##Using the source

Below are the commands you would use to get, restore, build and run the Npam library, tests and example applications.

### On Ubuntu

Install pam-devel: 
```
sudo apt-get install libpam0g-dev
```
From your home dir, clone repo: 
```
git clone https://github.com/camw/npam
```
Run the setup test account script to create a test user. This creates a user with username userxyz and a password of pwd123 which is used in the Npam test suite and can be used with the two example applications too: 
```
~/npam/test/setup_test_account.sh
```
Restore, build and run the Example.User app:
```
cd ~/npam/src/Npam.Example.User/
dotnet restore
sudo dotnet run
```
Restore, build and run the Example.Session app:
```
cd ~/npam/src/Npam.Example.Session/
dotnet restore
sudo dotnet run
```
Restore, build and run the Npam test suite:
```
cd ~/npam/test/Npam.Tests/
dotnet restore
sudo dotnet test
```
    

### On Fedora
Install pam-devel: 
```
sudo dnf -y install pam-devel
```
From your home dir, clone repo: 
```
git clone https://github.com/camw/npam
```
Run the setup test account script to create a test user. This creates a user with username userxyz and a password of pwd123 which is used in the Npam test suite and can be used with the two example applications too: 
```
~/npam/test/setup_test_account.sh
```
Restore, build and run the Example.User app:
```
cd ~/npam/src/Npam.Example.User/
dotnet restore
sudo /opt/dotnet/dotnet run
```
Restore, build and run the Example.Session app:
```
cd ~/npam/src/Npam.Example.Session/
dotnet restore
sudo /opt/dotnet/dotnet run
```
Npam test suite has known issues on Fedora 23. Doing a dotnet restore on the project results in the following error:
```
log  : Restoring packages for /root/npam/test/Npam.Tests/project.json...
log  : Failed to download package from 'https://api.nuget.org/v3-flatcontainer/system.runtime.handles/4.0.1-rc2-24027/system.runtime.handles.4.0.1-rc2-24027.nupkg'.
log  : Response status code does not indicate success: 404 (Not Found).
```
