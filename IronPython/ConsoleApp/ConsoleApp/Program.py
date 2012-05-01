colors = [['red', 'green', 'green', 'red' , 'red'],
          ['red', 'red', 'green', 'red', 'red'],
          ['red', 'red', 'green', 'green', 'red'],
          ['red', 'red', 'red', 'red', 'red']]

measurements = ['green', 'green', 'green' ,'green', 'green']


motions = [[0,0],[0,1],[1,0],[1,0],[0,1]]

sensor_right = 0.7

p_move = 0.8

def show(p):
    for i in range(len(p)):
        print p[i]

#DO NOT USE IMPORT
#ENTER CODE BELOW HERE
#ANY CODE ABOVE WILL CAUSE
#HOMEWORK TO BE GRADED
#INCORRECT

p = []

def sense_and_move(colors, measurements, sensor_right,motions, p_move):
    # initial prob distribution assuming uniform.
    size=0
    for i in range(len(colors)) :
        for j in range(len(colors[i])) :
            size += 1

    q = [[1./size for j in range(len(colors[i]))] for i in range(len(colors))]

    for i in range(len(measurements)) :
        q = move(colors, q, motions[i], p_move)
        q = sense(colors, q, measurements[i], sensor_right)

    return q

def sense(colors, p, Z, sensor_right):
    pHit = sensor_right;
    pMiss = 1 - pHit;

    q = [[p[i][j] * ((Z == colors[i][j]) * pHit + (1-(Z == colors[i][j])) * pMiss) for j in range(len(p[i]))] for i in range(len(p))]

    s=0
    for i in range(len(q)) :
        s += sum(q[i])

    q = [[q[i][j] / s for j in range(len(q[i]))] for i in range(len(q))]

    return q

def move(colors, p, U, p_move):
    #0,1 -> right. 0, -1 ->left 1,0 -> down -1,0 ->up. NO 1,1
    pExact = p_move
    pSame = 1 - p_move

    q = [[pExact * p[(i-U[0]) % len(p)][(j-U[1]) % len(p[i])] + pSame * p[i][j] for j in range(len(p[i]))] for i in range(len(p))]

    return q

p = sense_and_move(colors, measurements, sensor_right, motions, p_move)

#Your probability array must be printed 
#with the following code.

show(p)


