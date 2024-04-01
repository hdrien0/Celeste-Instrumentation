from enum import Enum

class SessionData(Enum):

    # Every frame you'll receive from the instrumeneted game a list of 26 values, which are the following:
    # Distances to the nearest object, calculated using raycasts
    DISTANCE_EAST = 0
    DISTANCE_NORTH_EAST = 1
    DISTANCE_NORTH = 2
    DISTANCE_NORTH_WEST = 3
    DISTANCE_WEST = 4
    DISTANCE_SOUTH_WEST = 5
    DISTANCE_SOUTH = 6
    DISTANCE_SOUTH_EAST = 7
    
    # Type of the nearest object
    # 0 : Hole
    # 1 : Transition to another room
    # 2 : Solid object
    # 3 : Spikes
    OBJECT_TYPE_EAST = 8
    OBJECT_TYPE_NORTH_EAST = 9
    OBJECT_TYPE_NORTH = 10
    OBJECT_TYPE_NORTH_WEST = 11
    OBJECT_TYPE_WEST = 12
    OBJECT_TYPE_SOUTH_WEST = 13
    OBJECT_TYPE_SOUTH = 14
    OBJECT_TYPE_SOUTH_EAST = 15

    # Player's velocity
    X_VELOCITY = 16
    Y_VELOCITY = 17

    # Player's state
    CAN_DASH = 18
    ON_GROUND = 19
    STAMINA = 20

    # Angle to the objective
    ANGLE_TO_OBJECTIVE = 21

    # Distance to the objective
    DISTANCE_TO_OBJECTIVE = 22

    # Metadata, should be used for normalisation purposes
    SECONDS_ELAPSED = 23
    LEVEL_DIAGONAL_LENGTH = 24
    NUMBER_OF_LEVELS_FINISHED = 25

class Inputs(Enum):
    # Then you'll have have to send back a list of 7 values, which are the following:
    # They are floats in the range [0,1]. If the value is greater than 0.5, the corresponding action will be performed.
    RIGHT = 0
    LEFT = 1
    UP = 2
    DOWN = 3
    JUMP = 4
    DASH = 5
    GRAB = 6
