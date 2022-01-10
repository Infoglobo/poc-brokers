APP_PORT=9000
MONITOR_PORT=9000
COMMIT=$$(git log --format="%h" -n 1)
TAG=$$(git describe --always)

.PHONY: deploy_consumer
deploy_consumer:
	cd src/consumer-rabbit-mq && make deploy

.PHONY: deploy_consumer_with_worker
deploy_consumer-with-worker:
	cd src/consumer-with-worker-rabbit-mq && make deploy

.PHONY: deploy_publisher
deploy_publisher:
	cd src/publisher-rabbit-mq && make deploy