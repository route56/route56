colors = [['green', 'green', 'green' ],
          ['green', 'red', 'green' ],
          ['green', 'green', 'green' ]]

measurements = ['red']
motions = [[0,0]]

sensor_right = 1.0
p_move = 1.0

expected = [[0.0, 0.0, 0.0],
            [0.0, 1.0, 0.0],
            [0.0, 0.0, 0.0]]

import homework1
import test

actual = homework1.sense_and_move(colors, measurements, sensor_right)

print expected
print actual

test.collection2d_assert(expected, actual);
