#!/bin/sh

# Ожидание запуска Kafka
echo "Ожидание запуска Kafka..."
until kafka-topics --bootstrap-server kafka:9092 --list > /dev/null 2>&1; do
  echo "Kafka не доступна, ожидаем..."
  sleep 5
done

# Создание топика 'orders'
kafka-topics --create --if-not-exists \
    --bootstrap-server kafka:9092 \
    --replication-factor 1 \
    --partitions 3 \
    --topic orders

# Создание топика 'payedOrder'
kafka-topics --create --if-not-exists \
    --bootstrap-server kafka:9092 \
    --replication-factor 1 \
    --partitions 3 \
    --topic payedOrder

# Вывод списка топиков
echo "Список топиков:"
kafka-topics --list --bootstrap-server kafka:9092

echo "Kafka топики созданы!"
