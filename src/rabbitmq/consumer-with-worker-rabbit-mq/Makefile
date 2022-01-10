APP_PORT=9000
MONITOR_PORT=9000
COMMIT=$$(git log --format="%h" -n 1)
TAG=$$(git describe --always)

.PHONY: build
build:
	docker build -t harbor.devops.valorpro.com.br/valor/valor-poc-${WHO}:${COMMIT} -f Dockerfile .

.PHONY: build_deployment
build_deployment:
	mkdir -p ./build
	cp -f ./deployment.yml ./build/deployment.yml
	sed -i 's/{RABBIT_USER}/'${RABBIT_USER}'/g' ./build/deployment.yml
	sed -i 's/{RABBIT_PASSWORD}/'${RABBIT_PASSWORD}'/g' ./build/deployment.yml	
	sed -i 's/{QUEUE}/'${QUEUE}'/g' ./build/deployment.yml	
	sed -i 's/{INSTANCE}/'${INSTANCE}'/g' ./build/deployment.yml	
	sed -i 's/{COMMIT}/'${COMMIT}'/g' ./build/deployment.yml	
	sed -i 's/{WHO}/'${WHO}'/g' ./build/deployment.yml	

.PHONY: push
push:
	docker push harbor.devops.valorpro.com.br/valor/valor-poc-${WHO}:${COMMIT}

.PHONY: deploy
deploy: build push build_deployment
	kubectl apply -f ./build/deployment.yml

.PHONY: build_local
build_local:
	docker build -t valor-poc-${WHO}:${COMMIT} -f Dockerfile .

.PHONY: run_local
run_local:
	docker run --rm \	    
	    -e RABBIT_USER="user" \
	    -e RABBIT_PASSWORD="VmwozUZ7jO" \	    
	    -e QUEUE="cotacoes" \	    
	    --name valor-poc-${WHO}-${INSTANCE} valor-poc-${WHO}-${INSTANCE}:${COMMIT}

.PHONY: stop_local
stop_local:
	docker stop valor-poc-${WHO}-${INSTANCE}
