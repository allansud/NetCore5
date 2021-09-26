pipeline {
    agent any

    stages {
        stage('Restore packages') {
            steps {
                echo 'Restoring...'
				echo "${workspace}"
				sh "dotnet restore ${workspace}/src/Application/Application.sln"
            }
        }
		stage('Build') {
            steps {
                echo 'Building...'
				sh "dotnet build ${workspace}/src/Application/Application.sln"
            }
        }
		stage("Tests"){
            steps {
                sh "dotnet test ${workspace}/src/Application/Application.sln /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput='/var/lib/jenkins/workspace/Net5_main/src/Application/Application.Tests/results/result.json' --no-build"
            }
        }
		stage('Sonarqube') {			
			environment {
				scannerHome = tool 'SonarQubeScanner'
			}
			steps {
				echo 'Analisando o que você fez...'
				withSonarQubeEnv('sonarqube') {
					sh "dotnet restore ${workspace}/src/Application/Application.sln"
					sh ("""dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:'Net5'""")
					sh "dotnet build ${workspace}/src/Application/Application.sln"
					sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll end"
				}
				timeout(time: 1, unit: 'MINUTES') {
					waitForQualityGate abortPipeline: true
				}
			}
		}
		stage(Report) {
			steps {
				sh "reportgenerator -reports: '/var/lib/jenkins/workspace/Net5_main/src/Application/Application.Tests/results/result.json' -targetdir:'coveragereport' -reporttypes:Html"
			}			
		}
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}