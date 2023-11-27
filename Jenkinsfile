pipeline {
    agent any
    
    stages {
        stage('Checkout') {
            steps {
                // Clonar o reposit�rio do GitHub
                checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'ghp_7Eoo8HYtVLBnGmAOxsyRqNPWTXNAj3YKs4y', url: 'https://github.com/guilherme-beltran/DollaroPoker.git']]])
            }
        }
        
        stage('Build') {
            steps {
                // Compilar o projeto ASP.NET (por exemplo, usando o MSBuild)
                bat 'msbuild.exe Backoffice.sln'
            }
        }
        
        stage('Test') {
            steps {
                // Executar testes (por exemplo, NUnit, MSTest, etc.)
                bat 'vstest.console.exe Backoffice.Tests.dll'
            }
        }
        
        stage('Deploy') {
            steps {
                // Implantar a aplica��o no servidor IIS
                bat 'xcopy /s/y C:\\inetpub\\wwwroot\\dollaro'
            }
        }
    }
    
    post {
        success {
            echo 'Implanta��o bem-sucedida!'
        }
        
        failure {
            echo 'Falha na implanta��o!'
        }
    }
}
