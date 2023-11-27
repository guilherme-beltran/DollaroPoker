pipeline {
    agent any
    
    stages {
        stage('Checkout') {
            steps {
                // Clonar o repositório do GitHub
                checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: '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', url: 'https://github.com/guilherme-beltran/DollaroPoker.git']]])
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
                // Implantar a aplicação no servidor IIS
                bat 'xcopy /s/y C:\\inetpub\\wwwroot\\dollaro'
            }
        }
    }
    
    post {
        success {
            echo 'Implantação bem-sucedida!'
        }
        
        failure {
            echo 'Falha na implantação!'
        }
    }
}
