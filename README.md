# Angular-ASP.net-App

<h2>Table Of Contents</h2>
<ul>
  <li><a href="#about">About the Project</a>
    <ul><li><a href="#build">Build with</a></li></ul>
  </li>
    <li><a href="#structure">File Structure</a></li>
  <li><a href="#screenshots">Screenshots</a> </li>
  
  
</ul>
<h2>What i have learnt from this project</h2>
<ul>
<li>
Getting familiar with ASP.net learnt its main advantages over the others. 
</li>
<li>
Using the mvc architecture in angular and mc in ASP.net.
</li>
<li>
Using Swagger.
</li>
<li>
Using AES Encryption and learnt more about hashing algorithms.
</li>
<li>
Logging all transactions using Log4Net Library.
</li>
</ul>

<h3 href="#build">Build with</h3>
<ul>
   <li><a href="https://angular.io/">Angular JS</a></li>
    <li>ASP.net</li>
    <li>Swagger</li>
 






</ul>

<h2 href="#structure">File Structure</h2>
 <div> 
  <pre>
Client
├── README.md
├── angular.json
├── LICENSE
├── package-lock.json
├── package.json
├── .gitignore
├── .browserslistrc
├── tsconfig.app.json
├── tsconfig.json  
├── .tsconfig.spec.json
├── src
│   ├── environments
│   ├── assets
│   ├── app
|   |   ├── components
|   |   |   ├── create
|   |   |   ├── update
|   |   |   ├── reset password
|   |   ├── models	
|   |   |   ├── names.model.ts
|   |   |   ├── resetData.model.ts
|   |   |   ├── userData.model.ts
|   |   |   ├── UserPersnonalData.model.ts
|   |   ├── services	
|   |   |   ├── user.service.ts
|   |   ├── app.component.css	
|   |   ├── app.component.html	
|   |   ├── app.component.spec.ts	
|   |   ├── app.component.ts	
|   |   ├── app.module.ts	
|   |   ├── app-routing.module.ts	
│   ├── main.ts
|   ├── polyfills.ts
│   └── test.ts
│   └── styles.css
|   |── favicon.ico	
|   |── index.html	
└── node_modules	
  </pre>
</div>

<h2 href="#screenshots">ScreenShots</h2>
<br>
<br>
<ol>
Simple UI
<img src="/ScreenShots/2.JPG">
<img src="/ScreenShots/3.JPG">
SWAGGER
<img src="/ScreenShots/1.JPG">

</ol>
<h2>Important Part For further contribution from anyone just pull request^^</h2>
<ol>
<li>
How could you make the password and the user data unknown for any person either the software engineers inside the company and the only user can access this data after signing up?
<blockquote>
for the first time in signing up the bare password will be hashed and stored in db and the user personal data will use this hashed password as an key for encryption and decryption and the hashing is used by a one way mathematical eq (will be explained in number theory course).
</blockquote>
</li>
<li>Advantages of ASP.net over PHP
<blockquote>
For Example search for SQL injection,Security
</blockquote>
</li>
<li>
You can prevent sql injection by using parameterized queries see the project and pull request it (it's an easy task).
</li>
<li>Read about AES Encryption Why and when its used and what is the difference and use cases between it and RSA Encryption</li>
<li>When updating the password  and hashed it will you first decrypt the user data with the old hashed password and encrypt them using the hashed password as if you are going to access these user data you will want the old hashed password? </li>

</ol>

