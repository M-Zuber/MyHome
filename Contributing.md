#Contribution Guide for [MyHome2013](https://github.com/M-Zuber/MyHome2013)

First off: thank you for showing intrest in this project.

## Table of Contents
- [Coding Guidelines](#coding-guidlines)
- [Contributing](#contributing)
    - [General](#general)
    - [Issues](#issues)
    - [Pull Requests](#pull-requests)
    - [Features](#features)
    - [Refactoring](#refactoring)
- [Areas of Focus](#areas-of-focus)

### Coding Guidelines

- [Microsoft Guidelines for C#](https://msdn.microsoft.com/en-us/library/ff926074.aspx).
- Use `LINQ Extension Methods` over `query syntax`.
 
 ```c#
 var result = list.Where(y => y.x); //Use this
 result = from y in z // Not this
		  where y.x
 		  select y;
 ```
 - Use `XML` style comments on `functions`, ``properties`, ect.

### Contributing
#### General
- Work should be `PR'ed` to the `Development` branch -unless it is for a feature that has a specific branch.

#### Issues
See the [Waffle Board](https://waffle.io/M-Zuber/MyHome2013) to get an idea of what issues are available.

#### Pull Requests
- Each `PR` should be connected to an issue.
  - It is okay to open the `issue` and `pull request` at the same time.

#### Features
In order to suggest a feature follow the following procedure:
- Open an issue which should contain the following elements:
  - A description.
  - Reasoning for adding the feature.
  - Screenshot/code example where applicable.
  - __Extra Credit__: Estimation of impact on code base.
- Discuss the feature in the issue.
- Once it is ready, the issue will have the `Ready` label applied.
- Ask to be assigned to the issue.
- Test your code.
- Keep commits small and with meaningful commit messages.
- Open `Pull Request`.
- Enjoy the new feature.

#### Refactoring
Please do not refactor until the project is better tested

### Areas of Focus
The following are in order of importance.

- Testing the project
- Closing the currently open issues
