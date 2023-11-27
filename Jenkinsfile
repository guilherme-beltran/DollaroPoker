pipeline {
    agent any
    
    stages {

        stage('Clean workspace'){
            steps{
                cleanWs()
            }
        }

        stage('Checkout') {
            steps { 
              git branch: 'master', credenciaisId: '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', url: 'https://github.com/guilherme-beltran/DollaroPoker.git' 
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
