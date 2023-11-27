pipeline {
    agent any

    environment {
        BRANCH_NAME = "build-${BUILD_NUMBER}"
        GITHUB_TOKEN = credentials('github-token') // Substitua 'github-token' pelo ID da credencial no Jenkins
    }

    stages {
        stage('Clean workspace') {
            steps {
                cleanWs()
            }
        }

        stage('Clone repository') {
            steps {
                script {
                    // Substitua 'your-repo-url' pela URL do repositório GitHub que você deseja clonar
                    sh "git clone https://${GITHUB_TOKEN}@github.com/guilherme-beltran/DollaroPoker.git"
                }
            }
        }

        stage('Create new branch') {
            steps {
                script {
                    sh "git checkout -b ${BRANCH_NAME}"
                    sh "git push origin ${BRANCH_NAME}"
                }
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
