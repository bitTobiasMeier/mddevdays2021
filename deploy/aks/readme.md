Deploy to AKS
==

1. Push the docker images to an azure container registry
2. Create an aks cluster and register the container registry.
3. Open the files platform.yaml and restaurantservice.yaml. Replace 'mddevdays.azurecr.io/' with the address of your container registry
4. Open a command prompt and deploy the yaml files with kubectl: 

kubectl deploy -f . 

5. Find the endpoint of the platform service:

kubectl get services


