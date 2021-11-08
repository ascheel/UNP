from kafka import KafkaProducer
import json
import time


def main():
    _topic = "MyNewTopic"

    print("Set up producer")
    producer = KafkaProducer(
        bootstrap_servers=[
            'localhost:29092'
        ],
        value_serializer=lambda x: json.dumps(x).encode('utf-8')
    )

    print("Send messages.")
    for count in range(25):
        print(f"Sending message {count}")
        data = {"number": count}
        producer.send(_topic, value=data)


if __name__ == "__main__":
    main()
