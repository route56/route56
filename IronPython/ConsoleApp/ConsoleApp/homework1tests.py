import homework1
import test

def hw1_1red():
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

    actual = homework1.sense_and_move(colors, measurements, sensor_right, motions, p_move)

    print expected
    print actual

    test.collection2d_assert(expected, actual);
    print 'Success hw1_1red'
    return

def hw1_2red():
    colors = [['green', 'green', 'green' ],
              ['green', 'red', 'red' ],
              ['green', 'green', 'green' ]]

    measurements = ['red']
    motions = [[0,0]]

    sensor_right = 1.0
    p_move = 1.0

    expected = [[0.0, 0.0, 0.0],
                [0.0, 0.5, 0.5],
                [0.0, 0.0, 0.0]]

    actual = homework1.sense_and_move(colors, measurements, sensor_right, motions, p_move)

    print expected
    print actual

    test.collection2d_assert(expected, actual);
    print 'success hw1_2red'
    return

def hw1_2red_inaccurate_sensor():
    colors = [['green', 'green', 'green' ],
              ['green', 'red', 'red' ],
              ['green', 'green', 'green' ]]

    measurements = ['red']
    motions = [[0,0]]

    sensor_right = 0.8
    p_move = 1.0

    expected = [[0.066, 0.066, 0.066],
                [0.066, 0.266, 0.266],
                [0.066, 0.066, 0.066]]

    actual = homework1.sense_and_move(colors, measurements, sensor_right, motions, p_move)

    print expected
    print actual

    test.collection2d_assert(expected, actual);
    print 'success hw1_2red_inaccurate_sensor'
    return

def hw1_2red_inaccurate_sensor_move_sense():
    colors = [['green', 'green', 'green' ],
              ['green', 'red', 'red' ],
              ['green', 'green', 'green' ]]

    measurements = ['red', 'red']
    motions = [[0,0], [0,1]]

    sensor_right = 0.8
    p_move = 1.0

    expected = [[0.033, 0.033, 0.033],
                [0.133, 0.133, 0.533],
                [0.033, 0.033, 0.033]]

    actual = homework1.sense_and_move(colors, measurements, sensor_right, motions, p_move)

    print expected
    print actual

    test.collection2d_assert(expected, actual);
    print 'success hw1_2red_inaccurate_sensor_move_sense'
    return

def hw1_2red_sensor_move_sense():
    colors = [['green', 'green', 'green' ],
              ['green', 'red', 'red' ],
              ['green', 'green', 'green' ]]

    measurements = ['red', 'red']
    motions = [[0,0], [0,1]]

    sensor_right = 1.0
    p_move = 1.0

    expected = [[0.0, 0.0, 0.0],
                [0.0, 0.0, 1.0],
                [0.0, 0.0, 0.0]]

    actual = homework1.sense_and_move(colors, measurements, sensor_right, motions, p_move)

    print expected
    print actual

    test.collection2d_assert(expected, actual);
    print 'success hw1_2red_sensor_move_sense'
    return

def hw1_2red_inaccurate_sensor_inaccurate_move():
    colors = [['green', 'green', 'green' ],
              ['green', 'red', 'red' ],
              ['green', 'green', 'green' ]]

    measurements = ['red', 'red']
    motions = [[0,0], [0,1]]

    sensor_right = 0.8
    p_move = 0.5

    expected = [[0.028, 0.028, 0.028],
                [0.072, 0.289, 0.463],
                [0.028, 0.028, 0.028]]

    actual = homework1.sense_and_move(colors, measurements, sensor_right, motions, p_move)

    print expected
    print actual

    test.collection2d_assert(expected, actual);
    print 'success hw1_2red_inaccurate_sensor_inaccurate_move'
    return

def hw1_2red_sensor_inaccurate_move():
    colors = [['green', 'green', 'green' ],
              ['green', 'red', 'red' ],
              ['green', 'green', 'green' ]]

    measurements = ['red', 'red']
    motions = [[0,0], [0,1]]

    sensor_right = 1.0
    p_move = 0.5

    expected = [[0.0, 0.0, 0.0],
                [0.0, 0.333, 0.666],
                [0.0, 0.0, 0.0]]

    actual = homework1.sense_and_move(colors, measurements, sensor_right, motions, p_move)

    print expected
    print actual

    test.collection2d_assert(expected, actual);
    print 'success hw1_2red_sensor_inaccurate_move'
    return

def hw1_bigexample():
    colors = [['red', 'green', 'green', 'red' , 'red'],
              ['red', 'red', 'green', 'red', 'red'],
              ['red', 'red', 'green', 'green', 'red'],
              ['red', 'red', 'red', 'red', 'red']]

    measurements = ['green', 'green', 'green' ,'green', 'green']


    motions = [[0,0],[0,1],[1,0],[1,0],[0,1]]

    sensor_right = 0.7

    p_move = 0.8

    expected = [[0.011059807427972008, 0.02464041578496803, 0.067996628067859166, 0.044724870458121582, 0.024651531216653717],
     [0.0071532041833209815, 0.01017132648170589, 0.086965960026646888, 0.079884299659980826, 0.0093506685084371859],
     [0.0073973668861116709, 0.0089437306704527025, 0.11272964670259773, 0.3535072295521271, 0.040655492078276761],
    [0.0091065058056464965, 0.0071532041833209815, 0.014349221618346571, 0.043133291358448934, 0.036425599329004729]]
    
    actual = homework1.sense_and_move(colors, measurements, sensor_right, motions, p_move)

    print expected
    print actual

    test.collection2d_assert(expected, actual);
    print 'success hw1_bigexample'
    return

# calling tests
hw1_1red()
hw1_2red()
hw1_2red_inaccurate_sensor()
hw1_2red_inaccurate_sensor_move_sense()
hw1_2red_sensor_move_sense()
hw1_2red_inaccurate_sensor_inaccurate_move()
hw1_2red_sensor_inaccurate_move()
hw1_bigexample()

print 'success'
