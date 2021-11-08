import os
import sys
import json
import yaml
import kafka

class UNPSend:
    def __init__(self):
        _basename = os.path.splitext(os.path.abspath(__file__))[0]
        self.settings_file = f"{_basename}.yml"

        self.settings = None
        if os.path.isfile("UNPSend.yml"):
            self.settings = yaml.safe_load(open(self.settings_file, "r").read())

        self.topic = self.settings["topic"]
