def num_ways_to_make_total(currencies, total):
	dp = [[0 for i in range(total+1)] for j in range(len(currencies)+1)]
	for i in range(len(currencies)+1):
		dp[i][0] = 1
	#dp[i][j] := ways to make j with the first i currencies
	for i,currency in enumerate(currencies):
		k = i+1
		for j in range(1,total+1):
			new_coin_sum = new_res = 0
			while new_coin_sum <= j:
				new_res += dp[i][j-new_coin_sum]
				new_coin_sum += currency
			dp[k][j] = new_res
	return dp[-1][total]

currencies = [1,2,5,10,20,50,100,200]
total = 200

result = num_ways_to_make_total(currencies, total)
print(result)
