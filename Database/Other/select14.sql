SELECT	maker, MAX(type)
FROM public."Product"
GROUP BY maker
HAVING COUNT(DISTINCT type) = 1 AND 
COUNT(DISTINCT model -> 'model') > 1