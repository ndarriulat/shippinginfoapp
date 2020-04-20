# shippinginfoapp

Welcome to my ShippingInfoApp!

Hope you find the code with good quality and cleanness as I do :)

I would like to start stating that this project does not fully cover the 4 scenarios presented in the Moteefe Coding Challenge. The reason for this is that I didn't quite understand Scenario 3 and 4, and since it was Sunday and I did not have anybody to ask to, I thought it would be better to focus on the Unit & Integration tests and code quality rather than modifying the logic of the program. I hope you can understand that approach that I followed.

As you will see in the code, I prefer not commenting much (except on one class in which I thought the method summaries were useful), and opt for having readable lines with self names explainable methods rather than that. I usually try to think about methods as texts in English language, that should be readable for almost anybody even though they would not know how to code.

Overall, I am quite happy with the code quality, however, I do believe that could have been much better. Still, as I didn't want to expend more time since I was already given some extra time to deliver it, I preferred not to take more time on working on this. 

That's why, I wrote a list with the **improvement points** that I thought of:
- The method `SetSuppliersDeliveryDates` is quite coupled with `SetShipmentList` and the `DeliveryDatesService`, and it was not very straightforward to think of isolated unit tests for that one.
- In addition to that, in any case, the Suppliers delivery dates should have been calculated within the foreach of the `SetShipmentList`. I preferred to keep on testing the logic rather than thinking an alternative for that.
- Countries and Suppliers should have been enums and not strings, and maybe Product names too. I would have definetly done that if continued working on this.
- Test data should have been stored in a resource file within the solution. The code from the method ProductDataSet was repeated quite a lot. Sorry about that one!
- Dictionaries could have been Lists of models. I went for the first option in order to save time.
- The parameters `(IList<Shipment> shipments, string region, IList<Product> products)` were passed through multiple methods. I could have created a sort of DTO to simplify that.
- Instead of using a list of Products, I could have used a CSV file as an input, but I found it not that relevant to invest time on mapping the lines of that file to objects in the code.
  
  That's all! I am looking forward to your feedback!


