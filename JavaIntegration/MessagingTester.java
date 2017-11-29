import DexterMessage;
import DexterMessageProducer;

public class MessagingTester {
	
	public static void main(String[] argv) {
		String username = "dexteradmin";
		String password = "Dexter123#";
		String host = "localhost";
		int port = 5672;
		
		String exchange = "Dexter Printing";
		String queue = "Test Queue";
		String routeKey = "Test";
		
		DexterMessage message = new DexterMessage();
		message.setId("TEST_ID");
		message.setFilePath("C:\\\\Users\\\\Erik Gabbard\\\\Desktop\\\\test.pdf");
		message.setOutputFile("");
		message.setDeleteAfterPrint(false);
				
	    DexterMessageProducer producer = new DexterMessageProducer(host, port, username, password);
	    
	    producer.PublishMessage(exchange, queue, routeKey, message.Serialize()); 
	   }

}
