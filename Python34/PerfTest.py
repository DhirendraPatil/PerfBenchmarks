import os
import json
import timeit
import sys
import gc
import operator
from collections import defaultdict
filepath="../datafile.txt"

#print (sys.platform)
dict=defaultdict(int)
def PerfTest():
    lines=open(filepath, 'r').readlines()   
    for line in lines:
        words=line.split(' ')
        for w in words:
            if w in dict:
                dict[w]+=1
            else:
                dict[w]=1   
    return dict

def PrintReport(dict):
    max_value = max(dict.values())
    max_keys = [k for k, v in dict.items() if v == max_value] 
    print(max_value, max_keys)   

#timeit.timeit('PerfTest()',number=1)
PerfTest()
PrintReport(dict)
first,second, *all=(sorted(dict.values(), reverse=True))
print(first, second)

