DELETE FROM public."Product"
	WHERE 
		model ->> 'hd' IN (SELECT MIN(CAST (model ->> 'hd' AS REAL)) FROM public."Product")
	OR
		model ->> 'ram' IN (SELECT MIN(CAST (model ->> 'ram' AS INTEGER)) FROM public."Product")