from dict2xml import dict2xml

class SessionParameters:
    def __init__(self):
        self.EndOnDeath = "true"
        self.EndOnLevelExit = "true"
        self.TimeoutSeconds = 5
        self.ObjectiveXCoordinate = 282
        self.ObjectiveYCoordinate = -24
        self.AreaKey = "1"
        self.Level = "1"
        self.AreaMode = "0"

    def SerializeToXML(self):
        return dict2xml(vars(self),wrap="SessionParameters")