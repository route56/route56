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
        #q = move(colors, q, motions[i], p_move)

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

# Why doesn't below work?
#q=[]
##r=[]
#for i in range(len(colors)) :
#    r=[]
#    for j in range(len(colors[i])) :
#        r.append(1./size)
#    q.append(r)
#    #r=[]
# http://stackoverflow.com/questions/2397141/how-to-initialize-a-two-dimensional-array-in-python
#bar = []
#for item in some_iterable:
#    bar.append(SOME EXPRESSION)
#bar = [SOME EXPRESSION for item in some_iterable]
