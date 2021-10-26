if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.AwsCredentials.AWS_ACCESS_KEY_ID') ]; then
    echo "AWS_ACCESS_KEY_ID is empty"
else 
    export AWS_ACCESS_KEY_ID=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.AwsCredentials.AWS_ACCESS_KEY_ID')    
fi

if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.AwsCredentials.AWS_SECRET_ACCESS_KEY') ]; then
    echo "AWS_SECRET_ACCESS_KEY is empty"
else 
    export AWS_SECRET_ACCESS_KEY=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.AwsCredentials.AWS_SECRET_ACCESS_KEY')   
fi

if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.AwsCredentials.AWS_DEFAULT_REGION') ]; then
    echo "AWS_DEFAULT_REGION is empty"
else 
    export AWS_DEFAULT_REGION=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.AwsCredentials.AWS_DEFAULT_REGION')    
fi

if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.EcsConfig.CLUSTER_NAME') ]; then
    echo "CLUSTER_NAME is empty"
else 
    export CLUSTER_NAME=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.EcsConfig.CLUSTER_NAME')  
fi

if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.EcsConfig.SERVICE_NAME') ]; then
    echo "SERVICE_NAME is empty"
else 
    export SERVICE_NAME=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.EcsConfig.SERVICE_NAME')    
fi

if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.EcsConfig.TASK_DEFINITION') ]; then
    echo "TASK_DEFINITION is empty"
else 
    export TASK_DEFINITION=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.EcsConfig.TASK_DEFINITION')    
fi

if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.DockerConfig.IMAGE_NAME') ]; then
    echo "IMAGE_NAME is empty"
    erro
else 
    export IMAGE_NAME=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.DockerConfig.IMAGE_NAME')    
fi

if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.DockerConfig.IMAGE_VERSION') ]; then
    echo "IMAGE_VERSION is empty"
    erro
else 
    export IMAGE_VERSION=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.DockerConfig.IMAGE_VERSION')    
fi

if [ -z $(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.EcsConfig.ECR_REPO') ]; then
    echo "ECR_REPO is empty"
    erro
else 
    export ECR_REPO=$(cat ./Brx.Unique.Profile/Brx.Unique.Profile.API/appsettings.homolog.json | jq -r '.Pipeline.EcsConfig.ECR_REPO')    
fi