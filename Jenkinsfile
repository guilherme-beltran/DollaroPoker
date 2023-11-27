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
            echo 'Implantação bem-sucedida!'
        }
        
        failure {
            echo 'Falha na implantação!'
        }
    }
}
