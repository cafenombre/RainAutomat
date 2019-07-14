    
::@echo off
set httpsgit="https://github.com/cafenombre/RainAutomat.git"

REM SET UP GIT ENV :
::git init
::git remote add origin %httpsgit%
::git remote -v
::pause

REM AutoCommit
set /p cmsg="Commit message: "

::track all files
git add .                           
::track deletes 
git add -u              
::commit with messages          
git commit -m "%cmsg%"      
::push to origin      
git push origin master              


pause