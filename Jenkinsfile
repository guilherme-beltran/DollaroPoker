pipeline {
    agent any
    
    stages {
        stage('Checkout') {
            steps {
                // Remova o step de checkout do GitSCM
                // Clonar o repositório do GitHub usando o comando git clone
                script {
                    def gitUrl = 'https://github.com/guilherme-beltran/DollaroPoker.git'
                    def credentialsId = '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'
                    
                    // Configurar as credenciais do Git
                    withCredentials([usernamePassword(credentialsId: credentialsId, usernameVariable: 'USERNAME',      passwordVariable: 'PASSWORD')]) {
                        sh """
                        git clone --branch master --single-branch --depth 1 ${gitUrl}
                        """
                    }
                }
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
