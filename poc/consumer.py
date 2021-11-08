from kafka import KafkaConsumer
import json
import time


def main():
    _topic = "MyNewTopic"

    print("Set up Consumer")
    consumer = KafkaConsumer(
        _topic,
        bootstrap_servers=[
            'localhost:29092'
        ],
        auto_offset_reset='earliest',
        enable_auto_commit=True,
        auto_commit_interval_ms=1000,
        group_id='UNP',
        value_deserializer=lambda x: json.loads(x.decode('utf-8'))
    )

    print("Receive messages.")
    for message in consumer:
        message = message.value
        print(message)


if __name__ == "__main__":
    main()
