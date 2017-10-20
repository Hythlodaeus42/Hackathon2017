## Hackathon 2017 - Ivory Tower

### Overview
This is the team git repository. We'll create folders at this level for multiple projects as required. 

### Code Management

#### Install git tools
https://git-for-windows.github.io/

https://desktop.github.com/

#### Clone repository
Choose local folder for your git repositories.
```
git clone https://github.com/Hythlodaeus42/Hackathon2017.git
```
This will create a local repository which has a remote repository defined as “origin”

#### Refresh from github
Refresh local repository
```
git checkout master
git pull origin master
```

#### Branching
All development will be done in feature branches. No development should be done on the master branch. We also want to keep branches limited to just a small number of changes, and short lived. This way we can quickly make independent changes without diverging too far. It also allows us to be selective in what changes we want to take into the **master** branch. 

From the **master** branch, create a feature branch:
```
git branch <feature branch name>
git checkout <feature branch name>
```

Commit changes to your local feature branch. 
```
git add .
git commit -m "<commit message>"
```

When ready, push the feature branch to GitHub
```
git push origin <feature branch name>
```

In GitHub, raise a pull request. 
https://help.github.com/articles/creating-a-pull-request/

Changes will be merged into the **master** branch.

After a new version of the **master** branch is available, you should refresh your local master.
```
git checkout master
git pull origin master
```

If your branch has been merged, you can now delete it.
```
git branch –d <feature branch name>
```

If a new master is available and you already have a feature branch, it is probably a good idea to sync your branch.
```
git checkout master
git pull origin master
git checkout <feature branch name>
git merge master –updates the feature branch with the latest master branch
```

#### Merge Conflicts
If a file is changed in two branches, and they cannot be automatically merged, a conflict will occur. Unity YAML files are susceptible to this. Short lived branches and frequent merges will minimise the risk, however this may still occur. 

Unity provides a tool to resolve conflicts called **UnityYAMLMerge**. Add the following to the **.git/config** file in your git repo. 

```
[merge]
	tool = unityyamlmerge

[mergetool "unityyamlmerge"]
	trustExitCode = false
	keepTemporaries = true
	keepBackup = false
	path = '<path to Unity install>\\Unity\\Editor\\Data\\Tools\\UnityYAMLMerge.exe'
	cmd = '<path to Unity install>\\Unity\\Editor\\Data\\Tools\\UnityYAMLMerge.exe' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
```

If you get a merge conflict, run
```
git mergetool
```
This should resolve the conflict allowing you to complete the merge.

More information can be found here:
https://docs.unity3d.com/Manual/SmartMerge.html
https://gist.github.com/Ikalou/197c414d62f45a1193fd
https://blogs.unity3d.com/2015/06/02/how-we-do-fast-and-efficient-yaml-merging/





