pipeline {
    agent any
    
    stages {

        stage('Clean workspace'){
            steps{
                cleanWs()
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
