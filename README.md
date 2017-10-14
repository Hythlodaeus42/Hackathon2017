## Hackathon 2017 - Ivory Tower

### Overview
This is the team git repository. We'll create folders at this level for multiple projects as required. 

### Code Management

#### Install git tools
https://git-for-windows.github.io/

https://desktop.github.com/

#### Close repository
Choose local folder for your git repositories.
```
git clone https://github.com/Hythlodaeus42/Hackathon2017.git
```

#### Branching
All development will be done in feature branches. No development should be done on the master branch.

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


