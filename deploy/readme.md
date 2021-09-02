Deploy to local cluster
==

1. Push the docker images to an azure container registry
2. Open the files platform.yaml and restaurantservice.yaml. 

- Replace 'mddevdays.azurecr.io/' with the address of your container registry
- Replace 'acr-secret' with the secret name in your local kubernetes cluster (https://docs.microsoft.com/de-de/azure/container-registry/container-registry-auth-kubernetes)

3. Open a command prompt and deploy the yaml files with kubectl: 

kubectl deploy -f . 

4. Map the platform service endpoint:

kubectl port-forward service/platform 8000:80 

5. Navigate to http://localhost:8000


