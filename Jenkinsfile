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
              git branch: 'master', url: 'https://github.com/MySocialBet/ApiBolao.git' 
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
