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
                                echo "Erro durante a restaura��o: ${e.message}"
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
                                echo "Erro durante a compila��o: ${e.message}"
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
                        // Verificar se a branch j� existe
                        def branchExists = bat(script: "git show-ref --verify --quiet refs/heads/${RELEASE_VERSION}", returnStatus: true) == 0
        
                        if (branchExists) {
                            echo "A branch ${RELEASE_VERSION} j� existe. Cancelando a publica��o."
                            currentBuild.result = 'FAILURE'
                            error("Branch ${RELEASE_VERSION} j� existe.")
                        } else {
                            // Criar a nova branch
                            bat "git checkout -b ${RELEASE_VERSION} origin/master"
                            branchCreated = true
        
                            // Atualizar a nova branch
                            bat "git pull origin master"
        
                            // Publicar
                            bat 'dotnet publish .\\Teste.sln --configuration Release --output C:\\inetpub\\wwwroot\\teste'
        
                            // Enviar para o reposit�rio
                            bat "git push origin ${RELEASE_VERSION}"
                        }
                    } catch (Exception e) {
                        echo "Erro durante a publica��o: ${e.message}"
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