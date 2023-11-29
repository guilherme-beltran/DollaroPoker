pipeline {
    agent any

    environment {
        RELEASE_VERSION = "release_version_1.21"
    }

    stages {
        stage('Checkout') {
            steps {
                script {
                    checkout scm
                }
            }
        }

        stage('Build and Restore') {
            parallel {
                stage('Restore') {
                    steps {
                        script {
                            try {
                                bat 'dotnet restore .\\Teste.sln'
                            } catch (Exception e) {
                                echo "Erro durante a restauração: ${e.message}"
                                currentBuild.result = 'FAILURE'
                                error(e.message)
                            }
                        }
                    }
                }

                stage('Build') {
                    steps {
                        script {
                            try {
                                bat 'dotnet build .\\Teste.sln --configuration Release --no-restore'
                            } catch (Exception e) {
                                echo "Erro durante a compilação: ${e.message}"
                                currentBuild.result = 'FAILURE'
                                error(e.message)
                            }
                        }
                    }
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    def branchCreated = false
        
                    try {
                        // Verificar se a branch já existe
                        def branchExists = bat(script: "git show-ref --verify --quiet refs/heads/${RELEASE_VERSION}", returnStatus: true) == 0
        
                        if (branchExists) {
                            echo "A branch ${RELEASE_VERSION} já existe. Cancelando a publicação."
                            currentBuild.result = 'FAILURE'
                            error("Branch ${RELEASE_VERSION} já existe.")
                        } else {
                            // Criar a nova branch
                            bat "git checkout -b ${RELEASE_VERSION} origin/master"
                            branchCreated = true
        
                            // Atualizar a nova branch
                            bat "git pull origin master"
        
                            // Publicar
                            bat 'dotnet publish .\\Teste.sln --configuration Release --output C:\\inetpub\\wwwroot\\teste'
        
                            // Enviar para o repositório
                            bat "git push origin ${RELEASE_VERSION}"
                        }
                    } catch (Exception e) {
                        echo "Erro durante a publicação: ${e.message}"
                        currentBuild.result = 'FAILURE'
                        error(e.message)
                    } finally {

                        // Mudar para outra branch antes de tentar excluir a branch
                        bat 'git checkout master'

                        // Excluir a branch apenas se ela foi criada com sucesso
                        if (branchCreated) {
                            bat "git branch -D ${RELEASE_VERSION}"
                        }
                    }
                }
            }
        }

    }
}