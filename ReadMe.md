# GiveCRM.Admin

## About GiveCRM.Admin

GiveCRM was designed and developed at the inaugural [GiveCampUK](http://www.givecrm.org.uk/) in London, October 2012.  The aim of the project is to provide a Customer Relationship Management (CRM) system that has been explicitly designed with charities in mind, with a focus on simplicity of use.  

GiveCRM.Admin is the Software-as-a-Service (SaaS) hosting layer of GiveCRM.  This is deployed to the cloud to provide managed instances of GiveCRM on behalf of charities that do not have the expertise to host their own instance.

## Contributing to GiveCRM.Admin

If you are new to GitHub, or have previously only used for single-developer projects, please first have a read of [How to GitHub: Fork, Branch, Track, Squash and Pull Request](http://gun.io/blog/how-to-github-fork-branch-and-pull-request/) from [gun.io](http://gun.io/).  

### Git Flow

You will notice when you first clone the GiveCRM.Admin repository that the default branch is `develop` rather than the more usual `master`.  We use the Git Flow branching model, [first described](http://nvie.com/posts/a-successful-git-branching-model/) by [nvie](http://www.twitter.com/nvie), so `master` moves on only at specific points.  There is a set of [helper scripts](https://github.com/nvie/gitflow) that will work on both Unix-based operating systems and Windows.  

Before you make any changes to your repository, configure your clone of GiveCRM.Admin for use with Git Flow by typing `git flow init`.  Pick a feature or bug to work on and create a new branch for that work by typing `git flow feature start <featurename>`.  This will create you a new branch for your work, and you can use git as usual from this point.  

Once your feature is finished, type `git flow feature publish <featurename>`.  This will push the feature up to your `origin` repository on GitHub and you will then be able to submit a pull request to have it merged into `develop`.  