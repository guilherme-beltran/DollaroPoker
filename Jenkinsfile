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
              git branch: 'master', credentialsId: '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', url: 'https://github.com/guilherme-beltran/DollaroPoker.git' 
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
