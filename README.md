# hatteland-poc

# CICD

## Setup Jenkins Docker image:
	
1. Pull jenkins image
	
		docker pull jenkins/jenkins:lts

2. Use jenkins image	
    
		docker run -u 0 -v /var/run/docker.sock:/var/run/docker.sock -v $(which docker):$(which docker) -p 8080:8080 -v /usr/var/jenkins_home jenkins/jenkins:lts

3. Create user group docker within jenkins docker container

    	groupadd docker

4. Add user Jenkins to user group ‘docker’

	    useradd -G docker Jenkins

5.  Let’s open localhost:8080 (or whatever you have chosen as the host port).Grab administrative password  from the Docker logs and paste it, Choose the suggested plugins option
	and Jenkins will start the installation of plugins.


## SonarQube with Jenkins Setup using Docker Image

 1. Install SonarQube Scanner Jenkins plugin (Manage Jenkins > Manage Plugins > Available)

 2. Download the official SonarQube image from Docker Hub

		docker pull sonarqube

 3. Start the server
      
	    docker run -d --name sonarqube -p 9000:9000 sonarqube
 
 4. Starts SonarQube on port 9000 and login to SonarQube with the default admin user and admin password

 5. SonarQube Scanner Configuration

  	i. access the Jenkins Docker container from a bash shell like this:
	  		
	    docker exec -it {Jenkins-CONTAINER ID } /bin/bash

	ii. create sonar-scanner directory under **both /var/jenkins_home and root directory of your jenkins Container**

	iii. download SonarQube Scanner onto the container from the sonar-scanner directory with wget:
			
	     wget https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-3.3.0.1492-linux.zip

	iv. unzip the Sonar Scanner binary:
			
	     unzip sonar-scanner-cli-3.3.0.1492-linux.zip

	v. in Jenkins:  update Jenkins to point to sonar-scanner binary (Manage Jenkins > Global Tool Configuration > SonarQube Scanner); you will need to uncheck “Install automatically” so you can explicitly set SONAR_RUNNER_HOME


## Configuring Jenkins and SonarQube

  1. In SonarQube: add webhook in SonarQube to point to Jenkins (Administration > Configuration > Webhooks); URL will be in the format http://<host_ip>:8080/sonarqube-webhook 

  2. In SonarQube, generate an access token that will be used by Jenkins (My Account > Security > Tokens)

  3. In Jenkins, add the SonarQube Server IP address and the access token` (Manage Jenkins > Configure System > SonarQube Servers); URL will be in the format http://<host_ip>:9000 
	   
	    * SonarQube access token can be found in sonarQube:
	    MyAccout(top right side corner) > security --> Token

 4. Create a sonarQube project or give jenkins pipeline script as follows:
            
	    bat 'dotnet sonarscanner begin /k:Hatteland-POC /d:sonar.host.url="http://<host-ip>:9000" /d:sonar.login=admin /d:sonar.password=admin'
            bat "dotnet build <project folder> --configuration Release"
            bat  "dotnet sonarscanner end /d:sonar.login=admin /d:sonar.password=admin"
 
 
## Configuring Jenkins and Github
 1. In jenkins add GitHUB server,(Manage Jenkins > Configure System > GitHub Server ) 
		
	 set API URL as https://api.github.com
	 
	 for credential: use secret text taken from github (github settings > Developer settings > personal Access token)
	
2. Add github-webhook for relevant github repository (git-repository > Webhooks > Add webhook )
		  
	 Payload URl : http://<host-ip>/github-webhook

      	 Content type: application.json

## Database connection
Add connections from the your specified IPs that will provides access to the databases.


## Install
   - [Install the .NET Core SDK in jenkins container](https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-ubuntu-1904)
   - [Install docker in Jenkins container](https://docs.docker.com/install/linux/docker-ce/debian/)
   - [Install node and angular cli]()
	

## Jenkins pipeline
As this is single branch repository, select pipeline as item and give pipeline name. (if your repository has multiple branches then you can move to the multibranch pipeline)
 After that mark the Github project and add your repository URL. Under the Buil Trigger, mark the GitHub hook tigger for GITScm polling as a option.
   
   -  should create two seperate pipelines for the client application(Angular) and the server project(.Net core)
   -  sperate jenkins pipelins scripts are attached in this repostory(jenkinsfile-client & jenkinsfile-server). 


## Usage

## Run tests

### Unit tests

### Integration tests


