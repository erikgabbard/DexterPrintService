package net.dextersolutions.messaging;

import com.google.gson.Gson;

public class printMessage {

	   private String Id;
	   private String FilePath;
	   private String OutputFile;
	   private boolean DeleteAfterPrint;

	   
		public String getId() {
			return Id;
		}
		
		
		public void setId(String id) {
			Id = id;
		}
		
		
		public String getFilePath() {
			return FilePath;
		}
		
		
		public void setFilePath(String filePath) {
			FilePath = filePath;
		}
		
		
		public String getOutputFile() {
			return OutputFile;
		}
		
		
		public void setOutputFile(String outputFile) {
			OutputFile = outputFile;
		}
		
		
		public boolean isDeleteAfterPrint() {
			return DeleteAfterPrint;
		}
		
		
		public void setDeleteAfterPrint(boolean deleteAfterPrint) {
			DeleteAfterPrint = deleteAfterPrint;
		}
		
		
		public String Serialize() {
	      Gson gson = new Gson();
	      return gson.toJson(this);
	   }
	}