import os
import sys
import json
import yaml
import kafka


class UNPSend:
    def __init__(self):
        _basename = os.path.splitext(os.path.abspath(__file__))[0]
        self.settings_file = f"{_basename}.yml"

        self.__settings = None
        self.__producer = None

        self.topic = self.settings["topic"]
        self.token = self.settings["token"]

    @property
    def settings(self):
        if not self.__settings:
            if os.path.isfile("UNPSend.yml"):
                self.settings = yaml.safe_load(open(self.settings_file, "r").read())
            else:
                raise ValueError("No settings file present.")
        return self.__settings
    
    @property
    def producer(self):
        if not self.__producer:
            self.__producer = kafka.KafkaProducer(
                bootstrap_servers=self.settings["bootstrap_servers"],
                value_serializer=lambda x: json.dumps(x).encode("utf-8")
            )
        return self.__producer

    def send_email(self, **kwargs):
        _protocol = "smtp"
        _sender = kwargs.get("sender")
        _recipient = kwargs.get("recipient")
        _subject = kwargs.get("subject")
        _body = kwargs.get("body", kwargs.get("message"))

    def send(self, **kwargs):
        _data = kwargs.copy()
        _data["token"] = self.token
        self.producer.send(
            self.topic,
            value=_data
        )
