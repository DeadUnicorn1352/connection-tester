# Description
this is a small dotnet app used to call an http endpoint and generate a toast(windows) notification if it fails for any reason.

made it because its a nice thing to have if you have an unreliable device that needs to be running (like a raspberry with a really bad RJ-45 cable)


# Run
$url = ""
.\build.ps1 && .\build\ServiceTester.exe $url

app will exit immediately and save output to logs file

# Plans
1. implement for linux (all if any suggestions are welcome)
2. make more flexible with args and stuff