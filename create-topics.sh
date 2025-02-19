#!/bin/bash

# �������� ������� Kafka
echo "�������� ������� Kafka..."
sleep 10

# �������� ������� orders � payedOrder
echo "������ Kafka ������..."
kafka-topics --create --topic orders --bootstrap-server kafka:9092 --partitions 1 --replication-factor 1
kafka-topics --create --topic payedOrder --bootstrap-server kafka:9092 --partitions 1 --replication-factor 1

# ����� ������ ���� �������
echo "������ ������� � Kafka:"
kafka-topics --list --bootstrap-server kafka:9092
