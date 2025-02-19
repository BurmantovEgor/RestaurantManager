#!/bin/bash

# Ожидание запуска Kafka
echo "Ожидание запуска Kafka..."
sleep 10

# Создание топиков orders и payedOrder
echo "Создаю Kafka топики..."
kafka-topics --create --topic orders --bootstrap-server kafka:9092 --partitions 1 --replication-factor 1
kafka-topics --create --topic payedOrder --bootstrap-server kafka:9092 --partitions 1 --replication-factor 1

# Вывод списка всех топиков
echo "Список топиков в Kafka:"
kafka-topics --list --bootstrap-server kafka:9092
