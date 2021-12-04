def counts_for_n_by_n(n=5):
	res = 1
	num = 3
	i = 0
	up_by = 2
	while num <= n**2:
		res += num
		i += 1
		if i%4==0:
			up_by += 2
		num += up_by
	return res

n=1001
result = counts_for_n_by_n(n)
print(result)
