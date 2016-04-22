# Borentra

A social network that makes it super simple for friends and family to borrow, rent, and trade items.

![Version 1](https://f.cloud.github.com/assets/25766/977424/71831404-06a3-11e3-98af-3d24dd354b5a.png)

### Environment setup

```
go to windowsazure.com
add your IP to SQL Server
git clone git@github.com:Borentra/Borrow.git && cd Borrow
open up the solution in Visual Studio 2013
Hit F5
http://localhost:9000
```

### Development Text Editor Settings

- CSharp should be set to 4 spaces
- HTML should be set to 2 spaces
- JavaScript should be set to 2 spaces

### SEO Strategy / Routing

User Profiles: /:username (example: borentra.com/jaime.bueza)
Items: /:guid/ipad-retina-3d

### Facebook Configuration (Permissions)

- email
- user about me

### Deployment (Web application / Services)

* Jenkins currently does all the deployment as long as we push to `master`
* Test continuous deplment
